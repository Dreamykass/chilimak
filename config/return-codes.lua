
-- general rcodes
General = {
    [000] = "Success.",
    [001] = "Unknown failure.",
    [002] = "Terminated without specifying an ecode.",
    [003] = "Terminated but couldn't find or open return-codes.lua.",
    [004] = "Given Chilimak directory isn't valid.",
    [005] = "Given Chilimak directory is valid, but doesn't contain chilimak_dir_beacon.",
    [006] = "Error or exception when reading or parsing return-codes.lua.",
    [007] = "Couldn't find an error code in return-codes.lua.",
    [008] = "Error or exception when searching for an rcode in return-codes.lua.",
    [011] = "Duplicate rcode found in return-codes.lua.",
}

-- kitchen rcodes
Kitchen = {
    [100] = "No args given - printed usage.",
}

-- lexer rcodes
Lexer = {

}

-- parser rcodes
Parser = {

}

-- generator rcodes
Generator = {

}
