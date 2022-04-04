function InformationModal(role) {
    switch (role) {
        case "SA":
            document.getElementById("modalBody").innerHTML = "<p>Ask Andrei</p>";
            break;
        case "A":
            document.getElementById("modalBody").innerHTML = "<p>Ask Andrei</p>";
            break;
        case "SU":
            document.getElementById("modalBody").innerHTML = "<p>Ask Andrei</p>";
            break;
        default:
            document.getElementById("modalBody").innerHTML = "<p>username: Basic</p> <p>password: User</p>";
    }
}