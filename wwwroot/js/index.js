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
