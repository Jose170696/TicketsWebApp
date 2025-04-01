document.addEventListener('DOMContentLoaded', function () {
    // Elementos principales
    const loginCard = document.querySelector('.card');
    const formInputs = document.querySelectorAll('.form-control');
    const buttons = document.querySelectorAll('.btn');
    const errorMessage = document.querySelector('.alert-danger');

    // Animación de entrada para la tarjeta
    if (loginCard) {
        loginCard.style.opacity = '0';
        loginCard.style.transform = 'translateY(20px)';
        loginCard.style.transition = 'opacity 0.8s ease, transform 0.8s ease';

        setTimeout(() => {
            loginCard.style.opacity = '1';
            loginCard.style.transform = 'translateY(0)';
        }, 100);
    }

    // Animación para los campos de entrada
    formInputs.forEach(input => {
        // Efecto al enfocar
        input.addEventListener('focus', function () {
            this.style.transition = 'transform 0.3s ease, box-shadow 0.3s ease';
            this.style.transform = 'translateY(-2px)';
            this.style.boxShadow = '0 4px 8px rgba(0, 123, 255, 0.1)';
        });

        // Restaurar al perder el foco
        input.addEventListener('blur', function () {
            this.style.transform = 'translateY(0)';
            this.style.boxShadow = 'none';
        });
    });

    // Efecto hover para botones
    buttons.forEach(button => {
        button.addEventListener('mouseenter', function () {
            this.style.transition = 'transform 0.3s ease';
            this.style.transform = 'translateY(-2px)';
        });

        button.addEventListener('mouseleave', function () {
            this.style.transform = 'translateY(0)';
        });
    });

    // Animación de sacudida para mensajes de error
    if (errorMessage) {
        errorMessage.style.animation = 'shake 0.5s ease';

        // Definir la animación de sacudida
        const style = document.createElement('style');
        style.textContent = `
            @keyframes shake {
                0%, 100% { transform: translateX(0); }
                10%, 30%, 50%, 70%, 90% { transform: translateX(-5px); }
                20%, 40%, 60%, 80% { transform: translateX(5px); }
            }
        `;
        document.head.appendChild(style);
    }

    // Animación para el icono de usuario
    const userIcon = document.querySelector('.fa-user-circle');
    if (userIcon) {
        userIcon.style.transition = 'transform 0.5s ease';

        // Pequeña animación de pulso
        setInterval(() => {
            userIcon.style.transform = 'scale(1.05)';
            setTimeout(() => {
                userIcon.style.transform = 'scale(1)';
            }, 500);
        }, 3000);
    }
});