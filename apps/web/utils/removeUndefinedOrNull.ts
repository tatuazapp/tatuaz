export const removeUndefinedOrNull = (obj: Record<string, unknown>) => {
  const newObj = { ...obj }
  Object.keys(newObj).forEach(
    (key) => newObj[key] == null && delete newObj[key]
  )
  return newObj
}
