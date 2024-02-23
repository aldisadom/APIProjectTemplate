using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Responses;

public class ItemListResponse
{
    public List<ItemResponse> Items { get; set; } = [];
}
