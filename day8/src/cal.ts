let display :HTMLInputElement =document.getElementById("display") as HTMLInputElement;

let buttons=document.querySelectorAll("BUTTON");

let expression :string='';

function appendvalue(value : string){
    expression+=value;
    display.value=expression;
}

function calculate() : void{
    try{
        const result : number= eval_ex(expression);
        display.value=result.toString();
        expression=result.toString();

    }catch(error){
        display.value="error";
        expression="";
    }
}
function eval_ex(expression : string) :number{
    if (/\/0(?!\d)/.test(expression)){
        throw new Error("Division BY Zero");
    }
    const result : number=eval(expression);
    return result;
}
function cleardisplay():void{
    display.value='';
    expression='';
}
buttons.forEach((button)=>
    button.addEventListener('click',()=>{
        const value : string=button.textContent || '';
        if (value==="C"){
            cleardisplay();
        }else if(value==="="){
            calculate();
        }else{
            appendvalue(value);
        }
    }))
