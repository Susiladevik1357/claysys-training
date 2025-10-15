const good=document.getElementById("good");
const cheap=document.getElementById("cheap");
const fast=document.getElementById("fast");
const message=document.getElementById("message");
function updateToggle(selected){
    const active=[good, cheap,fast].filter(x=>x.checked);
    if(active.length>2){
        selected.checked=false;
        message.textContent="Only two can be selected.";
        setTimeout(()=>message.textContent="",2000);
    }
}
[good, cheap, fast].forEach(toggleE1=>{
    toggleE1.addEventListener("change",()=>updateToggle(toggleE1));
});
