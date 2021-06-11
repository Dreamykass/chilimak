pub use crate::lexer::token::Token;
use logos::Logos;
pub mod token;

pub fn lex(src: &str) -> Vec<Token> {
    let tokens = Token::lexer(src);
    // let tokens = tokens.spanned().map(|(t, r)| Token { kind: t, span: r });

    // strip out comments
    let tokens = {
        let mut tokens_without_comments = Vec::<Token>::new();
        let mut star_comment_depth = 0;
        for token in tokens {
            match token {
                Token::StarCommentStart => star_comment_depth += 1,
                Token::StarCommentEnd => star_comment_depth -= 1,
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
