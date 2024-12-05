const cloud = document.getElementById("cloud");
const barraLateral = document.querySelector(".barra-lateral");
const spans = document.querySelectorAll("span");
const palanca = document.querySelector(".switch");
const circulo = document.querySelector(".circulo");
const menu = document.querySelector(".menu");
const main = document.querySelector("main");

menu.addEventListener("click",()=>{
    barraLateral.classList.toggle("max-barra-lateral");
    if(barraLateral.classList.contains("max-barra-lateral")){
        menu.children[0].style.display = "none";
        menu.children[1].style.display = "block";
    }
    else{
        menu.children[0].style.display = "block";
        menu.children[1].style.display = "none";
    }
    if(window.innerWidth<=320){
        barraLateral.classList.add("mini-barra-lateral");
        main.classList.add("min-main");
        spans.forEach((span)=>{
            span.classList.add("oculto");
        })
    }
});

palanca.addEventListener("click", () => {
    let body = document.body;

    // Alterna el modo oscuro en el body
    body.classList.toggle("dark-mode");

    // Alterna la clase 'prendido' para la animación del círculo
    circulo.classList.toggle("prendido");
});


cloud.addEventListener("click",()=>{
    barraLateral.classList.toggle("mini-barra-lateral");
    main.classList.toggle("min-main");
    spans.forEach((span)=>{
        span.classList.toggle("oculto");
    });
});

// Inicialización de Tooltips de Bootstrap
var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
    return new bootstrap.Tooltip(tooltipTriggerEl)
})

// Simulación de roles: visitante, cliente, administrador
const userRole = "client";

document.addEventListener("DOMContentLoaded", () => {
    const dashboard = document.getElementById("dashboard");
    const huespedes = document.getElementById("huespedes");
    const usuarios = document.getElementById("usuarios");
    const habitaciones = document.getElementById("habitaciones");
    const roles = document.getElementById("roles");

    if (userRole === "client") {
        roles.classList.remove("hidden");
        dashboard.classList.remove("hidden");
        habitaciones.classList.remove("hidden");
        huespedes.classList.remove("hidden");
        usuarios.classList.remove("hidden");
    } else {
        // Ocultar estos elementos si el rol no es "client"
        roles.classList.add("hidden");
        dashboard.classList.add("hidden");
        habitaciones.classList.add("hidden");
        huespedes.classList.add("hidden");
        usuarios.classList.add("hidden");
    }

    const menu = document.querySelector('.menu');
    const barraLateral = document.querySelector('.barra-lateral');

    if(menu) {
        menu.addEventListener('click', () => {
            barraLateral.classList.toggle('max-barra-lateral');
            if(menu.children[0].classList.contains('fa-angle-double-right')){
                menu.children[0].classList.remove('fa-angle-double-right');
                menu.children[0].classList.add('fa-angle-double-left');
            }else{
                menu.children[0].classList.add('fa-angle-double-right');
                menu.children[0].classList.remove('fa-angle-double-left');
            }
        });
    }

    // Selecciona todos los elementos con la clase 'dropdown-toggle'
    var dropdownToggles = document.querySelectorAll('.dropdown-toggle');

    // Añade un evento de clic a cada uno de ellos
    dropdownToggles.forEach(function (toggle) {
        toggle.addEventListener('click', function (e) {
            e.preventDefault(); // Prevenir el comportamiento predeterminado del enlace

            // Encuentra el elemento padre con la clase 'nav-item'
            var parent = this.closest('.nav-item');

            // Alterna la clase 'show' en el elemento padre
            parent.classList.toggle('show');

            // Cierra otros dropdowns abiertos
            dropdownToggles.forEach(function (otherToggle) {
                if (otherToggle !== toggle) {
                    var otherParent = otherToggle.closest('.nav-item');
                    otherParent.classList.remove('show');
                }
            });
        });
    });

    // Cierra el dropdown si se hace clic fuera de él
    document.addEventListener('click', function (e) {
        if (!e.target.closest('.nav-item')) {
            dropdownToggles.forEach(function (toggle) {
                var parent = toggle.closest('.nav-item');
                parent.classList.remove('show');
            });
        }
    });
});
