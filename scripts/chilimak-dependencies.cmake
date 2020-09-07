
find_package(fmt CONFIG REQUIRED)

set(SPDLOG_FMT_EXTERNAL)
find_package(spdlog CONFIG REQUIRED)

find_package(doctest CONFIG REQUIRED)

find_path(FPLUS_INCLUDE_DIRS "fplus/benchmark_session.hpp")

find_package(tomlplusplus CONFIG REQUIRED)


function(chilimak_add_dependencies_to_target CHILIMAK_FUNCTION_TARGET_NAME)
  target_link_libraries(${CHILIMAK_FUNCTION_TARGET_NAME}        PRIVATE   fmt::fmt)
  target_link_libraries(${CHILIMAK_FUNCTION_TARGET_NAME}        PRIVATE   spdlog::spdlog)
  target_link_libraries(${CHILIMAK_FUNCTION_TARGET_NAME}        PRIVATE   doctest::doctest)
  target_include_directories(${CHILIMAK_FUNCTION_TARGET_NAME}   PRIVATE   ${FPLUS_INCLUDE_DIRS})
  target_link_libraries(${CHILIMAK_FUNCTION_TARGET_NAME}        PRIVATE   tomlplusplus::tomlplusplus)
  
endfunction()
