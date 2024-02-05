export const convertToOrderBy = (field, orderBy) => {
    if( typeof field !== "string" ){
        return "";
    }
    return field.charAt(0).toUpperCase() + field.slice(1) + " " + (orderBy === "ascend" ? "asc" : "desc");
}