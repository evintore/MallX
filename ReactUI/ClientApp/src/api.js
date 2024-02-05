const host = "";

async function simpleRequest(data, path) {
  try {
    let response = await fetch(host + path, {
      method: "post",
      mode: "cors",
      credentials: "include",
      headers: {
        "Content-type": "application/json",
      },
      body: JSON.stringify(data),
    });
    return response.json();
  } catch (err) {
    console.log("Request error", err);
    return { isSuccess: false, message: err };
  }
}

function propertyFirstCharToUp(obj) {
  let nPayload = {};
  if (!obj) {
    return obj;
  }
  Object.keys(obj).map((key) => {
    let val = obj[key];
    if (Array.isArray(val)) {
      val = val.map((innerObj) => {
        return propertyFirstCharToUp(innerObj);
      });
    } else if (typeof val === "object") {
      val = propertyFirstCharToUp(val);
    }
    nPayload[key.charAt(0).toUpperCase() + key.substr(1)] = val;
  });
  return nPayload;
}

async function runAction(actionName, payload) {
  let nPayload = propertyFirstCharToUp(payload);
  return await simpleRequest(
    { ActionName: actionName, Params: nPayload },
    "action/runaction"
  );
}

const pickQueryKeyParam =
  (fn) =>
  ({ queryKey }) =>
    fn(queryKey);

const api = {
  save: async ({ query, data, method }) => {
    console.log("api save  ", { query, data, method });
    const res = await fetch(`api/${query}`, {
      method,
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    });
    return res;
  },
  getOneById: pickQueryKeyParam(async ([_, query, id]) => {
    const res = await (await fetch(`api/${query}/${id}`)).json();
    return res;
  }),
  getList: pickQueryKeyParam(
    async ([_, query, current, pageSize, order, searchKey]) => {
      const res = await (
        await fetch(
          `api/${query}?pageId=${current ?? 1}&pageSize=${pageSize ?? 20}${
            searchKey ? "&searchKey=" + searchKey : ""
          }` + (order ? "&orderBy=" + order : "")
        )
      ).json();
      return res;
    }
  ),
  getSubcategoryList: pickQueryKeyParam(
    async ([_, query, current, pageSize, order, searchKey, categoryId]) => {
      const res = await (
        await fetch(
          `api/${query}?pageId=${current ?? 1}&pageSize=${pageSize ?? 20}${
            searchKey ? "&searchKey=" + searchKey : ""
          }${categoryId ? "&categoryId=" + categoryId : ""}` +
            (order ? "&orderBy=" + order : "")
        )
      ).json();
      return res;
    }
  ),
  delete: async ({ query, id }) => {
    const res = await fetch(`api/${query}/${id}`, { method: "DELETE" });
    const string = await res.text();
    const json = string === "" ? {} : JSON.parse(string);
    return json;
  },
  getCountries: pickQueryKeyParam(async (_) => {
    const res = await (await fetch(`api/address/countries`)).json();
    return res;
  }),
  getCities: pickQueryKeyParam(async ([_, countryCode]) => {
    const res = await (
      await fetch(`api/address/cities?countryCode=${countryCode}`)
    ).json();
    return res;
  }),
  getTowns: pickQueryKeyParam(async ([_, cityCode]) => {
    const res = await (
      await fetch(`api/address/towns?cityCode=${cityCode}`)
    ).json();
    return res;
  }),
};

export default api;
