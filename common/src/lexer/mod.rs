pub mod logo;
use crate::lexer::logo::LogoKind;
pub use logo::Logo;
use logos::Logos;

pub fn lex(src: &str) -> Vec<Logo> {
    let lexed = LogoKind::lexer(src);
    lexed
        .spanned()
        .map(|(t, r)| Logo {
            raw_token_kind: t,
            span: r,
        })
        .collect()
}
