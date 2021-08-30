function render(modelAsJson) {
    const model = JSON.parse(modelAsJson);
    return `<html>
                <head>
                    <title>${model.heading}</title>
                </head>
                <body>
                    <h1>${model.heading}</h1>
                    <p>${model.preamble}</p>
                </body>
            </html>`;
}