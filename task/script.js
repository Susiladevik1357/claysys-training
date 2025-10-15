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
                // Only Good selected
                resetOthers(["good"]);
                message.textContent = "";
            }
        });

        cheap.addEventListener("change", () => {
            if (cheap.checked) {
                // Automatically select Good + Cheap
                good.checked = true;
                cheap.checked = true;
                fast.checked = false;
                message.textContent = "";
            }
        });

        fast.addEventListener("change", () => {
            if (fast.checked) {
                // Error if trying to select Fast with any others
                if (good.checked || cheap.checked) {
                    fast.checked = false;
                    message.textContent = "Cannot select Fast with Good or Cheap!";
                    setTimeout(() => message.textContent = "", 2000);
                } else {
                    // Only Fast is allowed alone
                    resetOthers(["fast"]);
                }
            }
        });
