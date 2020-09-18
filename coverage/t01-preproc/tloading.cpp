
#include <doctest/doctest.h>
#include "00-driver/_driver.hpp"
#include "01-preproc/_preproc.hpp"

TEST_CASE("loading source from file") {
  CHECK_THROWS(preproc::LoadSourceFromFile("invalid filename"));

  auto root = driver::FindRootDirectory();
  auto fname = root + "/tests/01-hello-world/main.kl";
  auto src = preproc::LoadSourceFromFile(fname);

  CHECK(src.pathname == fname);
  CHECK(src.content.find(std::string("fun Main()->Int")) != std::string::npos);
}
