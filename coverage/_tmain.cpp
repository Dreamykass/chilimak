
#define DOCTEST_CONFIG_IMPLEMENT_WITH_MAIN
#include <doctest/doctest.h>
#include "00-driver/_driver.hpp"

TEST_CASE("init spdlog") {
  driver::InitSpdlog();
}
