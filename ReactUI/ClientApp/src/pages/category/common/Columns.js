import EditableActionColumn from "../../../components/dataTable/EditableActionColumn";
import React from "react";

export const columns = (
  data,
  setData,
  setEditingKey,
  editingKey,
  form,
  onSave
) => {
  return [
    {
      title: "Alt Kategori",
      dataIndex: "subcategoryName",
      key: "subcategoryName",
      sorter: true,
      editable: true,
      onCell: (record, index) => ({
        record,
        editing: editingKey === index,
        dataIndex: "subcategoryName",
      }),
    },
    {
      title: "İşlem",
      dataIndex: "operation",
      render: (text, record, index) => (
        <EditableActionColumn
          query="subcategory"
          record={record}
          id={record.pkId}
          data={data}
          setData={setData}
          setEditingKey={setEditingKey}
          editingKey={editingKey}
          index={index}
          form={form}
          onSave={onSave}
        />
      ),
    },
  ];
};

export default columns;
