use std::ops::Range;

#[derive(Debug, PartialEq, Clone, serde::Serialize, serde::Deserialize)]
pub struct Token {
    pub kind: TokenKind,
    pub span: Range<usize>,
}

#[derive(logos::Logos, Debug, PartialEq, Clone, serde::Serialize, serde::Deserialize)]
#[rustfmt::skip]
pub enum TokenKind {
    #[error] #[regex(r"[ \t\r\n\f]+", logos::skip)] Error,

    #[token(".")] Dot, #[token(",")] Comma,
    #[token(":")] Colon, #[token(";")] SemiColon,

    #[token("(")] ParenL, #[token(")")] ParenR,
    #[token("{")] BraceL, #[token("}")] BraceR,
    #[token("<")] AngleL, #[token(">")] AngleR,
    #[token("[")] SquareL, #[token("]")] SquareR,

    #[token("<-")] ArrowL, #[token("->")] ArrowR,

    #[token("fun")] KwFun,
    #[token("ret")] KwRet,

    #[regex(r#"(_|[A-Za-z])([A-Za-z]|_|\d)*"#, |lexer| lexer.slice()[..].trim_end().to_owned())] 
    Identifier(String),
    
    #[regex(r#"\d+"#, |lexer| lexer.slice()[..].parse())] 
    LiteralNumber(i32),

    #[regex(r#"/\*"#)] StarCommentStart,
    #[regex(r#"\*/+"#)] StarCommentEnd,
    
}
