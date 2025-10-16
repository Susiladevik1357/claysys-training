const good = document.getElementById("good");
  const cheap = document.getElementById("cheap");
  const fast = document.getElementById("fast");
  const message = document.getElementById("message");

  function updateSelection() {
    const selected = [good, cheap, fast].filter(opt => opt.checked);
    message.textContent = "";
    if (selected.length > 2) {
      this.checked = false;
      message.textContent = "⚠️ Only two can be selected.";
    }
  }

  [good, cheap, fast].forEach(opt => {
    opt.addEventListener("change", updateSelection);
});
