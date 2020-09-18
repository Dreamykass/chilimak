
#include <doctest/doctest.h>
#include "00-driver/_driver.hpp"

TEST_CASE("termination") {
  CHECK_THROWS(driver::Terminate(4));
}
