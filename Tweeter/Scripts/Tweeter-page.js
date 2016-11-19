$(
    $.get("/Api/Tweet", function (data) {
        console.log(data);
    })
);

$("#submit-tweet").click(function() {
    $.post("/Api/Tweet", { message: "Here is a message", ImageURL: "Blank" });
});