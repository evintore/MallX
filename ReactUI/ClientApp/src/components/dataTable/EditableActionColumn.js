import React from "react";
import { Space, Button, Typography, Popconfirm } from "antd";
import { useMutation, useQueryClient } from "react-query";
import api from "../../api";

const EditableActionColumn = (props) => {
  //console.log("editable column props", props);
  // const [form] = Form.useForm();
  // const [editingKey, setEditingKey] = useState("");
  const { query, data, setData, record, id } = props;
  const setEditingKey = props.setEditingKey;
  const editingKey = props.editingKey;
  const index = props.index;
  const form = props.form;
  const onSave = props.onSave;
  const saveSubcategory = props.saveSubcategory;

  const queryClient = useQueryClient();
  const deleteRecord = useMutation(api.delete, {
    onSuccess: () => {
      queryClient.invalidateQueries(query);
    },
  });

  const deleteHandler = () => {
    setData((currentData) =>
      currentData.filter((curData) => curData.pkId !== id)
    );
    deleteRecord.mutate({ query, id });
  };

  const isEditing = () => {
    return index === editingKey;
  };

  const editable = isEditing(record);

  const editHandler = (record) => {
    form.setFieldsValue({
      ...record,
    });
    setEditingKey(index);
  };

  const cancelHandler = () => {
    setEditingKey("");
  };

  const saveHandler = async () => {
    try {
      const row = await form.validateFields();
      const newData = [...data];
      // const index = newData.findIndex((item) => key === item.pkId);

      if (index > -1) {
        const item = newData[index];
        const newItem = { ...item, ...row };
        newData.splice(index, 1, newItem);
        setData(newData);
        setEditingKey("");
        if (onSave) {
          onSave(newItem);
        }
      } else {
        newData.push(row);
        setData(newData);
        setEditingKey("");
      }
    } catch (errInfo) {
      console.log("Validate Failed:", errInfo);
    }
  };

  return editable ? (
    <Space size="middle">
      <Typography.Link
        onClick={() => saveHandler()}
        style={{
          marginRight: 8,
        }}
      >
        Kaydet
      </Typography.Link>
      <Popconfirm
        title="Değişiklik iptal edilsin mi?"
        onConfirm={cancelHandler}
      >
        <a>İptal</a>
      </Popconfirm>
    </Space>
  ) : (
    <Space size="middle">
      <Typography.Link
        disabled={editingKey !== ""}
        onClick={() => editHandler(record)}
      >
        <a>Düzenle</a>
      </Typography.Link>
      <Popconfirm
        placement="top"
        title="Kaydı silmek istiyor musunuz?"
        onConfirm={deleteHandler}
        okText="Evet"
        cancelText="Hayır"
      >
        <a>Sil</a>
      </Popconfirm>
    </Space>
  );
};

export default EditableActionColumn;
