import { UserSectionAreaWrapper, UserName, UserPhoto, UserIcon, UserIconStatus } from "./styles"

const UserSection = () => {
  const tmp = "Kk"

  return (
    <UserSectionAreaWrapper>
      <UserIcon>
        <UserPhoto />
        <UserIconStatus />
      </UserIcon>

      <UserName>Richard Brewl</UserName>
    </UserSectionAreaWrapper>
  )
}

export default UserSection
