pub mod token;

use crate::lexer::token::TokenKind;
use logos::Logos;
pub use token::Token;

pub fn lex(src: &str) -> Vec<Token> {
    let tokens = TokenKind::lexer(src);
    let tokens = tokens.spanned().map(|(t, r)| Token { kind: t, span: r });

    // strip out comments
    let tokens = {
        let mut tokens_without_comments = Vec::<Token>::new();
        let mut star_comment_depth = 0;
        for token in tokens {
            match token.kind {
                TokenKind::StarCommentStart => star_comment_depth += 1,
                TokenKind::StarCommentEnd => star_comment_depth -= 1,
                _ => {
                    if star_comment_depth == 0 {
                        tokens_without_comments.push(token);
                    }
                }
            }
        }
        tokens_without_comments
    };

    // map logos to tokens
    tokens
}
