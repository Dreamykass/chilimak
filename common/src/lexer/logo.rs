use std::ops::Range;

#[derive(Debug, PartialEq, Clone, serde::Serialize, serde::Deserialize)]
pub struct Logo {
    pub raw_token_kind: LogoKind,
    pub span: Range<usize>,
}

#[derive(logos::Logos, Debug, PartialEq, Clone, Copy, serde::Serialize, serde::Deserialize)]
#[rustfmt::skip]
pub enum LogoKind {
    #[error] #[regex(r"[ \t\r\n\f]+", logos::skip)] Error,

    #[token(".")] Dot,
    #[token(":")] Colon,
    #[token(",")] Comma,
    #[token(";")] Semi,

    #[token("(")] ParenL,
    #[token(")")] ParenR,
    #[token("{")] BraceL,
    #[token("}")] BraceR,
    #[token("<")] AngleL,
    #[token(">")] AngleR,
    #[token("[")] SquareL,
    #[token("]")] SquareR,

    #[token("<-")] ArrowL,
    #[token("->")] ArrowR,

    #[regex(r#"(_|[A-Za-z])([A-Za-z]|_|\d)*"#)] Identifier,

    #[regex(r#"\d+"#)] LiteralNumber,
    
}