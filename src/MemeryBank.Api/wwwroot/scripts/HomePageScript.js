document.querySelectorAll(".body button").forEach(button => {
    button.addEventListener("click", async () => {
        const target = button.dataset.target;

        const response = await fetch(target,  { Method: "Get" });
        const content = await response.text();

        document.querySelector("." + target).innerHTML = content;

    });
});