const slider = document.querySelector('.items');
let isDown = false;
let startX;
let scrollLeft;

const chevronLeft = document.querySelector(".chevron.left");
const chevronRight = document.querySelector(".chevron.right");
let position;
let start;
let end;

slider.addEventListener('mousedown', (e) => {
    isDown = true;
    slider.classList.add('active');
    startX = e.pageX - slider.offsetLeft;
    scrollLeft = slider.scrollLeft;
});
slider.addEventListener('mouseup', () => {
    isDown = false;
    slider.classList.remove('active');
});
slider.addEventListener('mousemove', (e) => {
    if (!isDown) return;
    e.preventDefault();
    const x = e.pageX - slider.offsetLeft;
    const walk = (x - startX) * 3; 
    slider.scrollLeft = scrollLeft - walk;
});
slider.addEventListener('mouseleave', () => {
    isDown = false;
    slider.classList.remove('active');
});

chevronLeft.addEventListener('mousedown', (e) => {
    position = slider.clientWidth + slider.scrollLeft;
    start = slider.clientWidth;
    if (position > start) {
        slider.classList.add('active');
        slider.scrollLeft -= 300;
    }
    else chevronLeft.classList.remove('chevron-hover');
});
chevronLeft.addEventListener('mouseup', (e) => {
    slider.classList.remove('active');
});
chevronLeft.addEventListener('mouseover', (e) => {
    position = slider.clientWidth + slider.scrollLeft;
    start = slider.clientWidth;

    if (position > start)
        chevronLeft.classList.add('chevron-hover');
});
chevronLeft.addEventListener('mouseout', (e) => {
    chevronLeft.classList.remove('chevron-hover');
});

chevronRight.addEventListener('mousedown', (e) => {
    position = slider.clientWidth + slider.scrollLeft;
    end = slider.scrollWidth;
    if (position < end-2) {
        slider.classList.add('active');
        slider.scrollLeft += 300;
        console.log(position < end-2);
    }
    else chevronRight.classList.remove('chevron-hover');
});
chevronRight.addEventListener('mouseup', (e) => {
    slider.classList.remove('active');
});
chevronRight.addEventListener('mouseover', (e) => {
    position = slider.clientWidth + slider.scrollLeft;
    end = slider.scrollWidth;

    if (position < end-2)
        chevronRight.classList.add('chevron-hover');
});
chevronRight.addEventListener('mouseout', (e) => {
    chevronRight.classList.remove('chevron-hover');
});

//const slider1 = document.querySelector('.items1');
//let scrollTop;
//let mousePos;
//let offsetTop;
//let offset;

//test.addEventListener('mousedown', (e) => {
//    isDown = true;
//    slider1.classList.add('active');







//    startX = e.pageY - slider1.scrollTop;
//    scrollTop = slider1.scrollHeight;



//    position = test.clientHeight + test.scrollTop;
//    end = test.scrollHeight;

//    console.log("-------------------------------------------\n")
//    console.log("test.clientHeight: " + test.clientHeight);
//    console.log("test.scrollTop: " + test.scrollTop);
//    console.log("test.scrollHeight: " + test.scrollHeight);
//    console.log("-------------------------------------------\n")

//    console.log("e.pageY: " + e.pageY + " test.offsetTop: " + test.offsetTop, "test.scrollTop: " + test.scrollTop, "test.clientHeight: " + test.clientHeight, "test.scrollHeight: " + test.scrollHeight);



//    //e.pageY
//    //test.offsetTop
//    //test.scrollTop
//    //test.clientHeight
//    //test.scrollHeight

//    mousePos = e.pageY;
//    mousePos = mousePos - (test.offsetTop + 21);
//    offset = test.clientHeight - mousePos;
//    mousePos = mousePos + offset;


//    var v = test.scrollTop + mousePos;

//    startX = e.pageY;

//    //console.log(v + " : " + test.scrollHeight);

//});
//test.addEventListener('mouseup', () => {
//    isDown = false;
//    slider1.classList.remove('active');
//});
//test.addEventListener('mousemove', (e) => {
//    if (!isDown) return;
//    e.preventDefault();
//    const x = e.pageY - slider1.scrollTop;
//    let walk = (x - startX) * 3;






//    mousePos = e.pageY;
//    offsetTop = test.offsetTop;
//    mousePos = mousePos - (offsetTop + 21);
//    offset = test.clientHeight - mousePos;
//    mousePos = mousePos + offset;

//    walk = test.scrollTop + mousePos;

//    //test.scroll(0, startX - walk);
//    console.log("startX: " + startX + " e.pageY: " + e.pageY + " difference: " + (e.pageY - startX));
//    test.scrollTop = test.scrollTop + (e.pageY - startX) / 10;





//});
//test.addEventListener('mouseleave', () => {
//    isDown = false;
//    slider1.classList.remove('active');
//});

