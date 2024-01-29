const checkboxList = [];

function closeAddModal() {
    const modal = document.getElementById("myModal");
    modal.style.display = "none";
}

function closeEditModal() {
    const modal = document.getElementById("edit-modal");
    modal.style.display = "none";
}

fetch("https://localhost:44352/transaction")
    .then((response) => {
        if (!response.ok) {
            throw new Error("An error ocurred");
        }
        console.log(response);
        return response.json();
    })
    .then((data) => {
        const tabela = document.getElementById("tabelaDados");

        const tbody = tabela.getElementsByTagName("tbody")[0];

        tbody.innerHTML = "";

        data.forEach((item) => {
            const newRow = tbody.insertRow();

            const cell1 = newRow.insertCell(0);
            const cell2 = newRow.insertCell(1);
            const cell3 = newRow.insertCell(2);
            const cell4 = newRow.insertCell(3);
            const cell5 = newRow.insertCell(4);
            const cell6 = newRow.insertCell(5);
            const cell7 = newRow.insertCell(6);
            const cell8 = newRow.insertCell(7);

            const data = new Date(item.date);

            const opcoesDeFormato = {
                day: "2-digit",
                month: "2-digit",
                year: "numeric",
                hour: "2-digit",
                minute: "2-digit",
            };
            const formatoData = new Intl.DateTimeFormat("pt-BR", opcoesDeFormato);
            const dataFormatada = formatoData.format(data);

            cell1.textContent = "#" + item.id;
            cell2.textContent = item.coin;
            cell3.textContent = dataFormatada;
            cell4.textContent = item.situation;
            cell5.textContent = item.quantity;
            cell6.textContent = item.price;
            cell7.textContent = item.profit;
            cell8.textContent = item.user.name;

            const checkbox = document.createElement("input");

            checkbox.type = "checkbox";
            checkbox.className = "checkbox";
            checkbox.dataset.id = item.id;
            checkbox.style.display = "none";

            cell1.insertBefore(checkbox, cell1.firstChild);

            checkboxList.push(checkbox);
        });
    })
    .catch((error) => {
        console.error("Erro:", error);
    });

document.addEventListener("DOMContentLoaded", function () {
    const checkedIds = [];

    const removeBtn = document.getElementById("removeBtn");

    const modalRemove = document.getElementById("remove-modal");

    const modalEdit = document.getElementById("edit-modal");

    const editBtn = document.getElementById("editBtn");

    const closeModalEditBtn = document.getElementById("closeModalEditBtn");

    const cancelModalEditBtn = document.getElementById("cancelModalEditBtn");

    closeModalEditBtn.addEventListener("click", function () {
        modalEdit.style.display = "none";
    });

    cancelModalEditBtn.addEventListener("click", function () {
        modalEdit.style.display = "none";
    });

    const closeModalRemovetBtn = document.getElementById("closeModalRemoveBtn");

    const cancelModalRemoveBtn = document.getElementById("cancelModalRemoveBtn");

    closeModalRemovetBtn.addEventListener("click", function () {
        modalRemove.style.display = "none";
    });

    cancelModalRemoveBtn.addEventListener("click", function () {
        modalRemove.style.display = "none";
    });

    removeBtn.addEventListener("click", function () {
        checkboxList.forEach((checkbox) => {
            checkbox.style.display = "inline-block";
        });
    });

    removeBtn.addEventListener("click", function () {
        checkboxList.forEach((checkbox) => {
            if (checkbox.checked) {
                checkedIds.push(checkbox.dataset.id);

                const row = checkbox.closest("tr");
                const cells = row.cells;

                modalRemove.classList.add("show");
                modalRemove.style.display = "block";

                const id = cells[0].textContent.replace("#", "");

                const saveChangesRemoveBtn = modalRemove.querySelector(
                    "#saveChangesRemoveBtn"
                );

                saveChangesRemoveBtn.addEventListener("click", function () {
                    const apiUrl = `https://localhost:44352/transaction/${id}`;

                    fetch(apiUrl, {
                        method: "DELETE",
                        headers: {
                            "Content-Type": "application/json",
                        },
                    })
                        .then((response) => response.json())
                        .then((data) => {
                            console.log("Resposta");
                            console.log(data);
                            alert("Changes saved!");
                            window.location.reload();
                            closeEditModal();
                        })
                        .catch((error) => {
                            console.error("Erro:", error);
                        });
                });
            }
        });
    });

    editBtn.addEventListener("click", function () {
        checkboxList.forEach((checkbox) => {
            checkbox.style.display = "inline-block";
        });
    });

    editBtn.addEventListener("click", function () {
        checkboxList.forEach((checkbox) => {
            if (checkbox.checked) {
                checkedIds.push(checkbox.dataset.id);

                const row = checkbox.closest("tr");
                const cells = row.cells;

                const id = cells[0].textContent.replace("#", "");
                const coin = cells[1].textContent;
                const date = cells[2].textContent;
                const situation = cells[3].textContent;
                const quantity = cells[4].textContent;
                const price = cells[5].textContent;
                const profit = cells[6].textContent;

                const modalEditContent = `

            <form id="editForm">
    <div class="mb-3">
      <label for="editCoin" class="form-label">Coin:</label>
      <input type="text" class="form-control" id="editCoin" value="${coin}">
    </div>
    <div class="mb-3">
      <label for="editDate" class="form-label">Date:</label>
      <input type="text" class="form-control" id="editDate" value="${date}">
    </div>
    <div class="mb-3">
      <label for="editSituation" class="form-label">Situation:</label>
      <input type="text" class="form-control" id="editSituation" value="${situation}">
    </div>
    <div class="mb-3">
      <label for="editQuantity" class="form-label">Quantity:</label>
      <input type="text" class="form-control" id="editQuantity" value="${quantity}">
    </div>
    <div class="mb-3">
      <label for="editPrice" class="form-label">Price:</label>
      <input type="text" class="form-control" id="editPrice" value="${price}">
    </div>
    <div class="mb-3">
      <label for="editProfit" class="form-label">Profit:</label>
      <input type="text" class="form-control" id="editProfit" value="${profit}">
    </div>
  </form>
          `;

                modalEdit.querySelector(".modal-body").innerHTML = modalEditContent;
                modalEdit.classList.add("show");
                modalEdit.style.display = "block";

                const saveChangesEditBtn = modalEdit.querySelector(
                    "#saveChangesEditBtn"
                );

                saveChangesEditBtn.addEventListener("click", function () {
                    const editedCoin = document.getElementById("editCoin").value;
                    const editedDate = document.getElementById("editDate").value;
                    const editedSituation =
                        document.getElementById("editSituation").value;
                    const editedQuantity = parseFloat(
                        document.getElementById("editQuantity").value
                    );
                    const editedPrice = document.getElementById("editPrice").value;
                    const editedProfit = document.getElementById("editProfit").value;

                    const editedData = {
                        price: editedPrice,
                        situation: editedSituation,
                        quantity: editedQuantity,
                        date: new Date(editedDate),
                        coin: editedCoin,
                        profit: editedProfit,
                    };

                    const apiUrl = `https://localhost:44352/transaction/${id}`;

                    const responseMessage = document.getElementById("responseMessage");

                    fetch(apiUrl, {
                        method: "PUT",
                        headers: {
                            "Content-Type": "application/json",
                        },
                        body: JSON.stringify(editedData),
                    })
                        .then((response) => response.json())

                        .then((data) => {
                            console.log("Resposta");
                            console.log(data);
                            alert("Changes saved!");
                            window.location.reload();
                        })
                        .catch((error) => {
                            console.error("Erro:", error);
                            responseMessage.textContent =
                                "An error ocurred, please check the console logs.";
                        });

                    modalEdit.style.display = "none";
                });
            }
        });
    });
});

document.addEventListener("DOMContentLoaded", function () {
    setTimeout(function () {
        const rows = document.querySelectorAll("#tabelaDados tbody tr");
        rows.forEach(function (row) {
            row.classList.add("fadeIn");
        });
    }, 100);
});

document.addEventListener("DOMContentLoaded", function () {
    const openModalBtn = document.getElementById("openModalBtn");
    const closeModalBtn = document.getElementById("closeModalAddBtn");
    const cancelModalBtn = document.getElementById("cancelModalAddBtn");
    const modal = document.getElementById("myModal");

    openModalBtn.addEventListener("click", function () {
        modal.style.display = "block";
    });

    closeModalBtn.addEventListener("click", function () {
        modal.style.display = "none";
    });

    cancelModalBtn.addEventListener("click", function () {
        modal.style.display = "none";
    });

    window.addEventListener("click", function (event) {
        if (event.target === modal) {
            modal.style.display = "none";
        }
    });
});

document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("myForm");
    const adicionarBtn = document.getElementById("addBtn");
    const responseMessage = document.getElementById("responseMessage");

    adicionarBtn.addEventListener("click", function () {
        const formData = new FormData(form);
        const formDataObject = {};
        formData.forEach((value, key) => {
            formDataObject[key] = value;
        });

        console.log(formDataObject);

        const apiUrl = "https://localhost:44352/transaction";

        fetch(apiUrl, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(formDataObject),
        })
            .then((response) => response.json())
            .then((data) => {
                alert("New transaction saved with success!");
                window.location.reload();
                closeAddModal();
            })

            .catch((error) => {
                console.error("Erro:", error);
                responseMessage.textContent =
                    "An error ocurred, please check the console logs.";
            });
    });
});
