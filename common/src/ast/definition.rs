use crate::ast::{Expression, Statement};
use serde::{Deserialize, Serialize};

#[derive(Debug, Clone, Serialize, Deserialize)]
pub enum Definition {
    FunctionDefinition(FunctionDefinition),
}

#[derive(Debug, Clone, Serialize, Deserialize)]
pub struct FunctionDefinition {
    pub name: String,
    pub return_type_name: String,
    pub body_statements: Vec<Statement>,
    pub body_last_expression: Option<Expression>,
}
