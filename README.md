
# ContaAPagar

API REST desenvolvida em **C# (.NET 9.0)** para controle de **contas a pagar**, aplicando regras de negócio de **atraso, multa e juros** com persistência em **PostgreSQL**.

## 🚀 Tecnologias Utilizadas

- C# (.NET 9.0)
- Entity Framework Core
- PostgreSQL
- Injeção de Dependência (DI)
- Swagger (para documentação da API)


## ⚙️ Funcionalidades

- Cadastro de contas a pagar
- Listagem e filtragem de contas
- Calculo de multas
- Valores Corrigidos

### 🧩 Serviços Disponíveis

#### ➕ Inclusão de Conta a Pagar
**POST** `/api/ContaAPagar`

Campos obrigatórios:
- `Nome`: texto  
- `ValorOriginal`: decimal  
- `DataVencimento`: data  
- `DataPagamento`: data  

Durante o cadastro, a API verifica se há atraso e aplica as seguintes regras:

| Dias de Atraso | Multa | Juros por dia |
|----------------|-------|----------------|
| até 3 dias     | 2%    | 0,1%           |
| até 5 dias     | 3%    | 0,2%           |
| acima de 5 dias| 5%    | 0,3%           |

---

#### 📋 Listagem de Contas Cadastradas
**GET** `/api/contaAPagar`

Retorna:
- `Id`
- `Nome`
- `ValorOriginal`
- `DataDeVencimento`
- `DataDePagamento`
- `ValorCorrigido`
- `DiasEmAtraso`
- `MultaAplicadaPercentual`
- `JurosAoDiaAplicadoPercentual`

