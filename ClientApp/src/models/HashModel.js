class HashModel
{
    #hash_func = null;

    constructor(hash_func)
    {
        this.#hash_func = hash_func;
    }

    get_hash(data)
    {
        return this.#hash_func(data)
    }
}

export default HashModel;
