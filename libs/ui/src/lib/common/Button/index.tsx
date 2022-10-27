import styled from "styled-components"

export const Button = styled.button`
  cursor: pointer;

  width: 150px;
  margin-top: 16px;
  margin-left: 16px;
  padding: 8px 16px;

  font-size: 16px;
  font-weight: 700;
  color: #fff;
  text-align: center;
  text-transform: uppercase;

  background: linear-gradient(90deg, #ff008c 0%, #ffcd1e 100%);
  border: none;
  border-radius: 8px;
  box-shadow: 0 8px 32px 0 rgba(31, 38, 135, 0.37);

  transition: all 0.15s ease 0s;

  &:hover {
    transform: translateY(-3px);
    box-shadow: 0 16px 70px 0 rgba(31, 38, 135, 0.37);
  }
`
