
#include "00-driver/_driver.hpp"
#include "00-driver/common-includes.hpp"

#include "spdlog/sinks/stdout_color_sinks.h"
#include <spdlog/sinks/basic_file_sink.h>

#include <stdio.h>
#include <time.h>

void driver::InitSpdlog(spdlog::level::level_enum _con_level,
                        spdlog::level::level_enum _file_level) {
  std::string time_now;
  {
    time_t now = time(0);
    struct tm tstruct;
    char buf[80];
    localtime_s(&tstruct, &now);
    strftime(buf, sizeof(buf), "%Y-%m-%d--%H-%M-%S", &tstruct);
    time_now = buf;
  }

  auto log_dir = driver::FindRootDirectory() + "/log/";
  auto log_fname = log_dir + time_now + ".log";

  std::vector<spdlog::sink_ptr> sinks;
  {
    auto color_logger = std::make_shared<spdlog::sinks::stdout_color_sink_mt>();
    color_logger->set_level(_con_level);
    sinks.push_back(color_logger);

    auto file_logger =
      std::make_shared<spdlog::sinks::basic_file_sink_mt>(log_fname);
    file_logger->set_level(_file_level);
    sinks.push_back(file_logger);
  }
  auto combined_logger = std::make_shared<spdlog::logger>(
    "combined_logger", begin(sinks), end(sinks));

  spdlog::register_logger(combined_logger);
  spdlog::set_default_logger(combined_logger);
  spdlog::set_pattern("[%T.%e]%^ [%=8l] %$[%24s:%3#] %v");
  spdlog::set_level(spdlog::level::trace);

  SPDLOG_INFO("spdlog initalized");
}
