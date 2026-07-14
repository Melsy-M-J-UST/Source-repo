const products = [
    { id: 1, name: "Laptop", description: "High-performance laptop", category: "Electronics", price: 999.99, quantity: 50 },
    { id: 2, name: "Desk Chair", description: "Ergonomic office chair", category: "Furniture", price: 249.99, quantity: 120 },
    { id: 3, name: "Notebook", description: "A4 ruled notebook, 200 pages", category: "Stationery", price: 4.99, quantity: 500 },
    { id: 4, name: "Headphones", description: "Noise-cancelling wireless headphones", category: "Electronics", price: 199.99, quantity: 75 },
    { id: 5, name: "Water Bottle", description: "Stainless steel insulated bottle", category: "Accessories", price: 24.99, quantity: 300 }
];

const tableBody = document.querySelector("#productTable tbody");

products.forEach(product => {
    const row = `
                <tr>
                    <td>${product.id}</td>
                    <td>${product.name}</td>
                    <td>${product.description}</td>
                    <td>${product.category}</td>
                    <td>${product.price}</td>
                    <td>${product.quantity}</td>
                </tr>
            `;
    tableBody.innerHTML += row;
});