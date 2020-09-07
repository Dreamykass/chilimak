cmake_minimum_required(VERSION 3.12.0)
project(chilimak_project VERSION 0.1.0 LANGUAGES CXX)

set(CMAKE_CXX_STANDARD 20)
set(CMAKE_CXX_STANDARD_REQUIRED ON)


# -----------------------------------------------------------------------------
# ------------------------- includes

include(CTest)

include(${CMAKE_SOURCE_DIR}/scripts/dkasses-compiler-options.cmake)

include(${CMAKE_SOURCE_DIR}/scripts/chilimak-dependencies.cmake)

include(${CMAKE_SOURCE_DIR}/scripts/set-core-files.cmake)
include(${CMAKE_SOURCE_DIR}/scripts/set-core-include-dirs.cmake)

# -----------------------------------------------------------------------------
# ------------------------- core library

add_library(chilimak_core ${CHILI_CORE_CPP_FILES} ${CHILI_CORE_HPP_FILES})

target_include_directories(chilimak_core PRIVATE ${CHILI_CORE_INCLUDE_DIRS})
dkass_set_compile_options(chilimak_core)
chilimak_add_dependencies_to_target(chilimak_core)


# -----------------------------------------------------------------------------
# ------------------------- tests

add_executable(first_test ${CMAKE_SOURCE_DIR}/tests/00-driver/_main.cpp)
add_test(NAME first_test COMMAND first_test)

target_include_directories(first_test PRIVATE ${CHILI_CORE_INCLUDE_DIRS})
dkass_set_compile_options(chilimak_core)
chilimak_add_dependencies_to_target(chilimak_core)


# -----------------------------------------------------------------------------
# ------------------------- fin
