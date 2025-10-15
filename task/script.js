const good = document.getElementById("good");
const cheap = document.getElementById("cheap");
const fast = document.getElementById("fast");
const message = document.getElementById("message");

function resetOthers(exceptIds = []) {
    [good, cheap, fast].forEach(cb => {
        if (!exceptIds.includes(cb.id)) cb.checked = false;
    });
}

good.addEventListener("change", () => {
    if (good.checked) {
        resetOthers(["good"]);
        message.textContent = "";
    }
});

cheap.addEventListener("change", () => {
    if (cheap.checked) {
        good.checked = true;
        cheap.checked = true;
        fast.checked = false;
        message.textContent = "";
    } else {
        good.checked = true;
        message.textContent = "";
    }
});

fast.addEventListener("change", () => {
    if (fast.checked) {
        if (good.checked || cheap.checked) {
            fast.checked = false;
            message.textContent = "Only two can be selected: Fast must be alone!";
            setTimeout(() => message.textContent = "", 2500);
        } else {
            resetOthers(["fast"]);
            message.textContent = "";
        }
    }
});
