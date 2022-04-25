if (window.addEventListener) {
    var keys = [],
        combination = "65,78,68,82,69,69,65";

    window.addEventListener("keydown", function (e) {
        keys.push(e.keyCode);

        if (keys.toString().indexOf(combination) >= 0) {
            document.getElementById('Andreea').classList.remove("visually-hidden");
            var randomImageNumber = Math.floor(Math.random() * 12) + 1;

            if (keys.length < 8) {
                $("#raccsInfoModal").modal('show');
                if (randomImageNumber > 9) {
                    document.getElementById('Andreea').setAttribute("src", "https://snowmireawscdn.s3.eu-central-1.amazonaws.com/AndreeaRaccs/racc" + randomImageNumber + ".jpg");
                }
                else {
                    document.getElementById('Andreea').setAttribute("src", "https://snowmireawscdn.s3.eu-central-1.amazonaws.com/AndreeaRaccs/racc0" + randomImageNumber + ".jpg");
                }
            }

            setAttribute(name, value)
            keys = [];
        };
    }, true);
};