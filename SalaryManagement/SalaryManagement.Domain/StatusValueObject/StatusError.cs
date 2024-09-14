using ErrorOr;

namespace SalaryManagement.Domain.StatusValueObject;

public class StatusError
{
    public static Error Validation() => Error.Validation(
        code: "Status.DataTidakValid",
        description: "Data yang diberikan tidak valid, hanya bisa menerima input : T0, T1, T2, T3, K0, K1, K2, K3");
}
