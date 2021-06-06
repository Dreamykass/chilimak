#[derive(Debug, PartialEq, Clone, serde::Serialize, serde::Deserialize)]
#[rustfmt::skip]
pub enum Token {
    Dot, Comma,
    Colon, SemiColon,

    ParenL, ParenR,
    BraceL, BraceR, 
    AngleL, AngleR,
    SquareL, SquareR,

    ArrowL, ArrowR,

    Identifier(String),

    LiteralNumber(i32),

}
