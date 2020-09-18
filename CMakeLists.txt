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

include(${CMAKE_SOURCE_DIR}/scripts/set-coverage-files.cmake)

# -----------------------------------------------------------------------------
# ------------------------- core library

add_library(chilimak_core SHARED ${CHILI_CORE_CPP_FILES} ${CHILI_CORE_HPP_FILES})

target_include_directories(chilimak_core PRIVATE ${CHILI_CORE_INCLUDE_DIRS})
dkass_set_compile_options(chilimak_core)
chilimak_add_dependencies_to_target(chilimak_core)


# -----------------------------------------------------------------------------
# ------------------------- generator

add_executable(chilimak_generator
    ${CMAKE_SOURCE_DIR}/generator/_gen_main.cpp
    ${CMAKE_SOURCE_DIR}/generator/abnf2pegtl.hpp
    )
target_include_directories(chilimak_generator 
    PRIVATE ${CMAKE_SOURCE_DIR}/generator
    )

dkass_set_compile_options(chilimak_generator)
chilimak_add_dependencies_to_target(chilimak_generator)

target_link_libraries(chilimak_generator PRIVATE chilimak_core)
target_include_directories(chilimak_generator PRIVATE ${CHILI_CORE_INCLUDE_DIRS})


# -----------------------------------------------------------------------------
# ------------------------- tests

add_executable(chilimak_coverage ${CHILI_COVERAGE_FILES})
add_test(NAME chilimak_coverage COMMAND chilimak_coverage)

dkass_set_compile_options(chilimak_coverage)
chilimak_add_dependencies_to_target(chilimak_coverage)

target_link_libraries(chilimak_coverage PRIVATE chilimak_core)
target_include_directories(chilimak_coverage PRIVATE ${CHILI_CORE_INCLUDE_DIRS})


# -----------------------------------------------------------------------------
# ------------------------- fin
