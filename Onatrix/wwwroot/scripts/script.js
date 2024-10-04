function mobileMenu() {
    var navMenu = document.querySelector('.nav-menu');

    navMenu.classList.toggle('active');
    console.log(navMenu.classList);
}

document.querySelector('.btn-menu').addEventListener('click', mobileMenu);