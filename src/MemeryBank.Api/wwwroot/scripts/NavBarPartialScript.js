const container = document.querySelector(".navbar-partial");

document.querySelectorAll(".navbar button").forEach(button => {
    button.addEventListener("click", async () => {
        const target = button.dataset.target;

        // If same content is already shown, hide it (toggle off)
        if (container.dataset.current === target && container.style.display === "block") {
            container.style.display = "none";
            container.dataset.current = "";
            return;
        }

        // Fetch new content from the server
        const response = await fetch(target);
        const content = await response.text();

        // Show and update
        container.innerHTML = content;
        container.style.display = "block";
        container.dataset.current = target;
    });
});