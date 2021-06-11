use crate::ast::Expression;
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, Serialize, Deserialize)]
pub enum Statement {
    ExpressionStatement(Expression), // Expression SemiColon
    ReturnStatement(Expression),
}
