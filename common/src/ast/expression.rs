use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, Serialize, Deserialize)]
pub enum Expression {
    LiteralExpression(LiteralExpression),
}



#[derive(Debug, Clone, Serialize, Deserialize)]
pub struct LiteralExpression {
    value: i32,
}
