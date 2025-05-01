
    let idAtualizar = null;
    // Funçao mensagens
    function exibirErro(mensagem) {
        const errorMessageDiv = document.getElementById("errorMessage");
        errorMessageDiv.innerHTML = mensagem;
        errorMessageDiv.style.display = "block";
    }
    function exibirCerto(mensagem) {
        const RightMessageDiv = document.getElementById("RightMessage");
        RightMessageDiv.innerHTML = mensagem;
        RightMessageDiv.style.display = "block";
    }

    // Função para limpar a lista de erros
    function limparErro() {
        const errorMessageDiv = document.getElementById("errorMessage");
        errorMessageDiv.style.display = "none";
        const RightMessageDiv = document.getElementById("errorMessage");
        RightMessageDiv.style.display = "none";
    }

    // Função para adicionar produto
    async function adicionarProduto() {
        const nome = document.getElementById("produtoNome").value;
        const quantidade = parseInt(document.getElementById("produtoQuantidade").value);
        const valor = parseFloat(document.getElementById("produtoValor").value);

        if (!nome || quantidade <= 0 || valor <= 0) {
            exibirErro("Todos os campos devem ser preenchidos corretamente.");
            return;
        }

        const novoProduto = { nome, quantidade, valor };

        try {
            const response = await fetch("http://localhost:5043/api/estoque", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(novoProduto)
            });

            if (!response.ok) {
                throw new Error("Erro ao adicionar produto.");
            }
            exibirCerto("Produto adicionado com sucesso.");
            carregarEstoque();
        } catch (error) {
            exibirErro(error.message);
        }
    }

    async function removerProduto(id) {
    try {
        const response = await fetch(`http://localhost:5043/api/estoque/${id}`, {
            method: "DELETE"
        });

        if (!response.ok) {
            throw new Error("Erro ao remover produto.");
        }
        exibirCerto("Produto deletado com sucesso.");
        carregarEstoque();
    } catch (error) {
        exibirErro(error.message);
    }
}

    function prepararAtualizacao(id, nome, quantidade, valor) {
        document.getElementById("produtoNome").value = nome;
        document.getElementById("produtoQuantidade").value = quantidade;
        document.getElementById("produtoValor").value = valor;
        idAtualizar = id;
}
    async function atualizarProduto() {
    if (!idAtualizar) {
        exibirErro("Selecione um produto para atualizar clicando em 'Editar'.");
        return;
    }

    const nome = document.getElementById("produtoNome").value;
    const quantidade = parseInt(document.getElementById("produtoQuantidade").value);
    const valor = parseFloat(document.getElementById("produtoValor").value);

    if (!nome || quantidade <= 0 || valor <= 0) {
        exibirErro("Todos os campos devem ser preenchidos corretamente.");
        return;
    }

    const produtoAtualizado = { id: idAtualizar, nome, quantidade, valor };

    try {
        const response = await fetch(`http://localhost:5043/api/estoque/${idAtualizar}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(produtoAtualizado)
        });

        if (!response.ok) {
            throw new Error("Erro ao atualizar produto.");
        }

        idAtualizar = null; 
        exibirCerto("Produto atualizado com sucesso.");
        carregarEstoque();
    } catch (error) {
        exibirErro(error.message);
    }
}

    async function carregarEstoque() {
        try {
            const response = await fetch("http://localhost:5043/api/estoque");
            const estoque = await response.json();

            const tabela = document.getElementById("estoqueTabela").getElementsByTagName('tbody')[0];
            tabela.innerHTML = "";

            estoque.forEach(item => {
                const row = tabela.insertRow();
                row.innerHTML = `
                <td>${item.nome}</td>
                <td>${item.quantidade}</td>
                <td>R$ ${item.valor.toFixed(2)}</td>
                <td><button class="btn btn-vermelho" onclick="removerProduto(${item.id})">Remover</button></td>
                <td><button class="btn btn-amarelo" onclick="prepararAtualizacao(${item.id}, '${item.nome}', ${item.quantidade}, ${item.valor})">Editar</button></td>`;
            });

            limparErro(); 
        } catch (error) {
            exibirErro("Erro ao carregar estoque.");
        }
    }

window.onload = carregarEstoque;
