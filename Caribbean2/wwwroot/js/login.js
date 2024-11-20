function showLogin() {
    document.querySelector('.login-container').style.right = "0";
    document.querySelector('.register-container').style.right = "100%";
    document.querySelector('.forgot-password-container').style.right = "200%";
    document.getElementById("loginBtn").classList.add("white-btn");
    document.getElementById("registerBtn").classList.remove("white-btn");
    document.querySelector('.login-container').style.opacity = "1";
    document.querySelector('.register-container').style.opacity = "0";
    document.querySelector('.forgot-password-container').style.opacity = "0";
}

function showRegister() {
    document.querySelector('.login-container').style.right = "200%";
    document.querySelector('.register-container').style.right = "0";
    document.querySelector('.forgot-password-container').style.right = "100%";
    document.getElementById("loginBtn").classList.remove("white-btn");
    document.getElementById("registerBtn").classList.add("white-btn");
    document.querySelector('.login-container').style.opacity = "0";
    document.querySelector('.register-container').style.opacity = "1";
    document.querySelector('.forgot-password-container').style.opacity = "0";
}

function showForgotPassword() {
    document.querySelector('.login-container').style.right = "300%";
    document.querySelector('.register-container').style.right = "200%";
    document.querySelector('.forgot-password-container').style.right = "0";
    document.getElementById("loginBtn").classList.remove("white-btn");
    document.getElementById("registerBtn").classList.remove("white-btn");
    document.querySelector('.login-container').style.opacity = "0";
    document.querySelector('.register-container').style.opacity = "0";
    document.querySelector('.forgot-password-container').style.opacity = "1";
}