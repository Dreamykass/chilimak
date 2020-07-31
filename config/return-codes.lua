
ReturnCodes = {
-- general rcodes
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

-- kitchen rcodes
    [100] = "No args given - printed usage.",
    [101] = "Given project path does not exist or is not valid.",
    [102] = "Given project path does not contain kitchen-recipe.lua.",
    [103] = "Error or exception when reading kitchen-pipeline.lua.",
    [104] = "None of the specified possible paths for a step in the pipeline is valid.";

-- lexer rcodes


-- parser rcodes


-- generator rcodes

}
