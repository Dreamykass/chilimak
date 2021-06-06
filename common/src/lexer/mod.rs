pub mod logo;
pub mod token;

use crate::lexer::logo::LogoKind;
pub use logo::Logo;
use logos::Logos;
pub use token::Token;

pub fn lex(src: &str) -> Vec<Token> {
    let logos = LogoKind::lexer(src);
    let logos = logos.spanned().map(|(t, r)| Logo { kind: t, span: r });

    // strip out comments
    let logos = {
        let mut logos_without_comments = Vec::<Logo>::new();
        let mut star_comment_depth = 0;
        for logo in logos {
            match logo.kind {
                LogoKind::StarCommentStart => star_comment_depth += 1,
                LogoKind::StarCommentEnd => star_comment_depth -= 1,
                _ => {
                    if star_comment_depth == 0 {
                        logos_without_comments.push(logo);
                    }
                }
            }
        }
        logos_without_comments
    };

    let mut x = string_interner::StringInterner::default();
    let s = x.get_or_intern_static("siema");

    // map logos to tokens
    logos
        .into_iter()
        .map(|logo: Logo| -> Token {
            match logo.kind {
                LogoKind::Error => panic!(
                    "during lexing, encountered an error logo around span {}-{}: '{}'",
                    logo.span.start,
                    logo.span.end,
                    src[logo.span.clone()].to_string()
                ),

                LogoKind::Dot => Token::Dot,
                LogoKind::Comma => Token::Comma,
                LogoKind::Colon => Token::Colon,
                LogoKind::SemiColon => Token::SemiColon,
                LogoKind::ParenL => Token::ParenL,
                LogoKind::ParenR => Token::ParenR,
                LogoKind::BraceL => Token::BraceL,
                LogoKind::BraceR => Token::BraceR,
                LogoKind::AngleL => Token::AngleL,
                LogoKind::AngleR => Token::AngleR,
                LogoKind::SquareL => Token::SquareL,
                LogoKind::SquareR => Token::SquareR,
                LogoKind::ArrowL => Token::ArrowL,
                LogoKind::ArrowR => Token::ArrowR,
                LogoKind::Identifier => Token::Identifier(src[logo.span].to_string()),
                LogoKind::LiteralNumber => Token::LiteralNumber(src[logo.span].parse().unwrap()),

                LogoKind::StarCommentStart => panic!("comments should be stripped out by now!"),
                LogoKind::StarCommentEnd => panic!("comments should be stripped out by now!"),
            }
        })
        .collect()
}
