
if(NOT DEFINED DKASSES_COMPILE_OPTIONS_MSVC)
  set(DKASSES_COMPILE_OPTIONS_MSVC
    /MP /permissive- /W4 /w14640 /w14242 /w14254 /w14263 /w14265 /w14287 
    /we4289 /w14296 /w14311 /w14545 /w14546 /w14547 /w14549 /w14555 /w14619 
    /w14640 /w14826 /w14905 /w14906 /w14928
  )
endif()

if(NOT DEFINED DKASSES_COMPILE_OPTIONS_ELSE)
  set(DKASSES_COMPILE_OPTIONS_ELSE
    -Wall -Wextra -Wextra -Wshadow -Wnon-virtual-dtor -Wold-style-cast
    -Wcast-align -Wunused -Woverloaded-virtual -Wpedantic -Wconversion
    -Wsign-conversion -Wmisleading-indentation -Wduplicated-cond
    -Wduplicated-branches -Wlogical-op -Wnull-dereference -Wuseless-cast
    -Wdouble-promotion -Wformat=2
  )
endif()

function(dkass_set_compile_options DKASSES_FUNCTION_TARGET_NAME)
  if(MSVC)
    target_compile_options(${DKASSES_FUNCTION_TARGET_NAME} 
                           PRIVATE 
                           ${DKASSES_COMPILE_OPTIONS_MSVC}
    )
  else()
    target_compile_options(${DKASSES_FUNCTION_TARGET_NAME} 
                           PRIVATE 
                           ${DKASSES_COMPILE_OPTIONS_ELSE}
    )
  endif()
endfunction()