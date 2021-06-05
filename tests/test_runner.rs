use common::lexer::logo::LogoKind;

#[test]
fn test_runner() {
    for entry in walkdir::WalkDir::new("tests") {
        let entry = entry.unwrap();
        let path = entry.path().to_string_lossy();

        if path.ends_with(".kl") {
            let source = std::fs::read_to_string(&*path).unwrap();
            let lexer_output_test = std::fs::read_to_string(path.to_string() + ".lex.json");

            if let Ok(lexer_output_test) = lexer_output_test {
                println!("lex test: {}", path);

                let lexer_output = common::lexer::lex(&source);
                let lexer_output: Vec<LogoKind> =
                    lexer_output.into_iter().map(|t| t.raw_token_kind).collect();

                assert_eq!(
                    lexer_output,
                    serde_json::from_str::<Vec<LogoKind>>(&lexer_output_test)
                        .expect(&*format!("invalid json at path: {}", path)),
                    "lexer output test failed; path: {}",
                    path
                )
            }

            // if let Ok() = {
            //    println!("par test: {}", path);
            // }
        }
    }
}
