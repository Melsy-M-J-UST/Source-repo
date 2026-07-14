function generateInvoice() {

    let products = [
        { name: "Barbie Doll", price: 20, qty: document.getElementById("q1").value },
        { name: "Calculator", price: 30, qty: document.getElementById("q2").value },
        { name: "Mobile Phone", price: 40, qty: document.getElementById("q3").value },
        { name: "LG DVD", price: 50, qty: document.getElementById("q4").value }
    ];

    let hasItem = false;
    let invoiceHTML = `
        <h2>INVOICE</h2>
        <table border="1" cellpadding="10">
        <tr>
            <th>Product</th>
            <th>Quantity</th>
            <th>Price</th>
            <th>Total</th>
        </tr>
    `;

    let grandTotal = 0;

    products.forEach(p => {
        let qty = parseInt(p.qty);

        if (qty > 0) {
            hasItem = true;
            let total = qty * p.price;
            grandTotal += total;

            invoiceHTML += `
                <tr>
                    <td>${p.name}</td>
                    <td>${qty}</td>
                    <td>${p.price}</td>
                    <td>${total}</td>
                </tr>
            `;
        }
    });

    if (!hasItem) {
        let errorWindow = window.open("", "Error", "width=300,height=150");

        errorWindow.document.write(`
        <h3 style="text-align:center; margin-top:40px;">
            No Item Selected
        </h3>
        <div style="text-align:center; margin-top:20px;">
        <button onclick="window.close()">OK</button>
        </div>
    `);

        return;
    }

    invoiceHTML += `
        <tr>
            <td colspan="3"><b>Total Amount</b></td>
            <td><b>${grandTotal}</b></td>
        </tr>
        </table>
    `;

    let newWindow = window.open("", "Invoice", "width=600,height=400");
    newWindow.document.write(invoiceHTML);
}