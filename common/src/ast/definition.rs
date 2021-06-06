#[derive(Debug, Clone, serde::Serialize, serde::Deserialize)]
pub enum Definition {
    FunctionDefinition { name: String },
}
