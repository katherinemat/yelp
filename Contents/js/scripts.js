$(document).ready(function(){
  $("form#new-restaurant").submit(function(event){
    document.getElementById("start-date").defaultValue = "1800-01-01";
  });
  $("form#update").submit(function(event){
    document.getElementById("restaurant-date").defaultValue = "1800-01-01";
  });
});
