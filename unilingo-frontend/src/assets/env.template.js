(function(window) {
    window.env = window.env || {};
  
    // Environment variables
    window["env"]["apiUrl"] = "${UI_API_BASE_URL}";
    window["env"]["debug"] = "${UI_DEBUG}";
  })(this);