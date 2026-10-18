using AlertaAracaju.Dominio.Entidades;

namespace AlertaAracaju.Dominio.Interface;

// Interface que informa quais os métodos existem
public interface IOcorrenciaRepository
{
    // Método que adiciona uma ocorrencia (tem como propriedade a instancia da classe "Ocorrencia")
    void AdicionarOcorrencia(Ocorrencia ocorrencia);

    // Método que lista todas as ocorrencias (retorna uma lista de ocorrencia "IEnumerable<>")
    IEnumerable<Ocorrencia> ListarTodos();
    //void EditarOcorrencia(Ocorrencia dadosAtualizados);
}
