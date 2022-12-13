const { createProxyMiddleware } = require('http-proxy-middleware');

const context = [
    "/api/controller",
    "/api/Teste",
    "/api/Aluno",
    "/api/Disciplina"
];

module.exports = function (app) {
    const appProxy = createProxyMiddleware(context, {
        target: 'https://localhost:7047',
        secure: false
    });

    app.use(appProxy);
};
