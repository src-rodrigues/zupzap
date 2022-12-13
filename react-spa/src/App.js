import React, { useEffect, useState } from 'react';
import {
  Button,
  Col,
  Container,
  Input,
  InputGroup,
  InputGroupText,
  ListGroup,
  ListGroupItem,
  Row
} from 'reactstrap';
import { httpClient } from './HTTPClient';

export default function App() {
  /*
  const testeAPI = () => {
    httpClient()
      .get('Teste/Testando')
      .then(r => {
        debugger;
        return r.json();
      })
      .then(r => {
        alert(r.msg);
      })
      .catch(e => {
        console.log(e);
        alert('Deu erro.');
      });
  };
  */

  function obterMensagens() {
    httpClient()
      .get('/mensagens')
      .then(r => r.json())
      .then(rJson => {
        console.log(rJson);
        setMensagensState(rJson);
      })
      .catch(e => {
        console.log(e);
        alert('Deu erro!');
      });
  }

  function excluirMensagem(id) {
    httpClient()
      .delete(`/mensagens/${id}`)
      .then(r => r.json())
      .then(rJson => {
        console.log(rJson);
        obterMensagens();
      })
      .catch(e => {
        console.log(e);
        alert('Deu erro!');
      });
  }

  function salvarMensagem() {
    httpClient()
      .post('/mensagens', {
        usuario: 'Victor',
        texto: msgState
      })
      .then(r => r.json())
      .then(rJson => {
        console.log(rJson);
        obterMensagens();
        setMsgState('');
      })
      .catch(e => {
        console.log(e);
        alert('Deu erro!');
      });
  }
  const [msgState, setMsgState] = useState('');
  const [mensagensState, setMensagensState] = useState([]);

  /*
  setTimeout(_ => {
    obterMensagens();
  }, 5000);
  */

  useEffect(_ => {
    obterMensagens();
  }, []);

  /*
  const deleteButton = (
    <Button onClick={_ => excluirMensagem()}>Excluir</Button>
  );
  */
  return (
    <Container>
      <h1>Zap Fipp</h1>
      <Row>
        <Col>
          <ListGroup>
            {mensagensState.map(item => (
              <ListGroupItem key={item.id}>
                <div>{item.usuario}</div>
                <div>{item.texto}</div>
                <div>{item.data}</div>
                {item.usuario === 'Victor' ? (
                  <Button
                    color='danger'
                    onClick={_ => excluirMensagem(item.id)}
                  >
                    Excluir
                  </Button>
                ) : (
                  ''
                )}
              </ListGroupItem>
            ))}
          </ListGroup>
        </Col>
      </Row>
      <InputGroup>
        <InputGroupText>Digite a mensagem</InputGroupText>
        <Input
          type='text'
          value={msgState}
          onChange={e => setMsgState(e.target.value)}
        ></Input>
        <Button color='primary' onClick={_ => salvarMensagem()}>
          Salvar
        </Button>
      </InputGroup>
    </Container>
  );
}
